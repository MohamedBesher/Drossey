 var jsonFile = sessionStorage.getItem('jsonFile');
        var lesson_id=  sessionStorage.getItem('lessonId');
var quizData, queBackUp = [], quizOptions = {
    "report": false,
    "feedback": false,
    "randomaize":false
},currQ = 1,score = [];
var cAnswerSoundPath="../sound/Quiz_right"//set the path of correct sound without extention 
,wAnswerSoundPath="../sound/Quiz_wrong"//set the path of wrong sound without extention 
,clickSoundPath="../sound/Speech_Misrecognition"//set the path of nav sound without extention 
,checkSoundPath="../sound/Speech_On";//set the path of ckeck or view sound without extention 
var arrOfIndex = function (a) {
    var arr = [];
    $.each($(a), function (ind, value) {
        arr.push($($(a)[ind]).index());
    })
    
    return arr
}
var checkDuplicateArr = function (a, b) {
    var back = true;
    if (a.length == b.length) {
        // //.log(b)
        // console.log(a)
        $.each(a, function (index, value) {
            
            if (value != b[index]) {
                back = false
            }
            
        })
        
    } else {
        return false
    }
    return back;
}
var checkChoise = function (a) {
    var b = function (c) {
        switch (c) {
            case 1:
                return " الإختيار الاول"
                break;
            case 2:
                return " الإختيار الثاني"
                break;
            case 3:
                return " الإختيار الثالث"
                break;
            case 4:
                return " الإختيار الرابع"
                break;
            case 5:
                return " الإختيار الخامس"
                break;
            case 6:
                return " الإختيار السادس"
                break;
            case 7:
                return " الإختيار السابع"
                break;
            case 8:
                return " الإختيار الثامن"
                break;
            case 9:
                return " الإختيار التاسع"
                break;
            case 10:
                return " الإختيار العاشر "
                break;
        }
    }
    if (!(Array.isArray(a))) {
        return b(a)
    } 
    else {
        var arr = []
        for (var i = 0; i < a.length; i++) {
            arr.push(b(a[i]))
        }
        //console.log(arr.toString())
        return arr.toString()
    }
}
/*queBackUp is array of object {"state":enable/disable,"degree":10,"answer":true} 
currQ is the current question
score[c]={"coAnwser":"--","stAnwser":"--","answerState":true,"answerType":"essay"}
*/
function loadScript(path, callback) {
    var done = false;
    var scr = document.createElement('script');
    scr.onload = handleLoad;
    scr.onreadystatechange = handleReadyStateChange;
    scr.onerror = handleError;
    scr.src = path;
    document.body.appendChild(scr);
    
    function handleLoad() {
        if (!done) {
            done = true;
            callback(path, "ok");
        }
    }
    
    function handleReadyStateChange() {
        var state;
        if (!done) {
            state = scr.readyState;
            if (state === "complete") {
                handleLoad();
            }
        }
    }
    
    function handleError() {
        if (!done) {
            done = true;
            callback(path, "error");
        }
    }
    return scr;
} 

function playAudio(pathWithOutExtention){
    $('<audio id="audioDemo" controls preload="auto"><source src="'+pathWithOutExtention+'.mp3" type="audio/mp3"><source src="'+pathWithOutExtention+'.ogg" type="audio/ogg"></audio>')[0].play()

}

var currFileName =function() {
    //location.pathname.split('/').slice(-1)[0]
    var pageName=location.href.split("/").slice(-1)[0]
    return pageName.substring(0, pageName.length - 5);
}
function start() {
    // this key code to link with player and load json 
   /*    loadScript('../Player/js/redundant.js', function (a, b) {}).onload = function () {
        loadScript('../Player/lessons/' + lessonID + '/media/data_pages_'+currFileName()+'.js', function (a, b) {}).onload = function () {*/
       
   
         loadScript("../Data/Lessons/"+lesson_id+"/"+jsonFile+".js",function (a, b) {}).onload = function ()
        {
            init();
        };
 //   }
        
   
    //end key code 
};

function init() {
    // defined quiz options
    if (quiz["report"] != undefined && quiz["report"].enabled != undefined) {
        if (quiz["report"].enabled) {
            quizOptions.report = true
        }
    }
    if (quiz["feedback"] != undefined && quiz["feedback"]) {
        quizOptions.feedback = true
    }
    if (quiz["randomaize"] != undefined && quiz["randomaize"]) {
        quizOptions.randomaize = true
    }
    if (quiz["questions"].length > 0) {
        quizData = quiz["questions"]
        if(quizOptions.randomaize){quizData.sort(function(){return Math.random()-.5})}
        loadQuiz(0)
        //loadQuizNav(quizData.length);
        loadQuizItemNav(quizData.length)
    } else {
        $(".frame_body").html('<h1 class="quiz_title" style="text-align: center;"> لا يوجد اسئلة لعرضها  </h1>')
        
    }
}

function loadQuiz(c) {
    if (queBackUp[c] == undefined || quizData[c].type == "essay") {
        var coAnswerClass = "";
        $("#quiz_body").html('')
        $("#quiz_body").html('<h1 class="quiz_title">' + quizData[c].body + '</h1>')
        $('#q_head').text(quizData[c].header)
        $('.frame_head').show()
        switch (quizData[c].type) {
            case "essay":
                /*essay start*/
                $('#text_modal .modal-title').text(quizData[c].feedback_title)
                $('#text_modal .frame_text').text(quizData[c].rightfeedback)
                
                $('<textarea class="form-control" rows="9" placeholder="اكتب اجابتك هنا  ..."></textarea>').appendTo("#quiz_body")
                $('<div class="clearfix"></div> <button type="button" class="view_quiz_btn" >عرض الإجابة</button> ').appendTo("#quiz_body")
                $('.view_quiz_btn').bind('click', function (event) {
                   playAudio(checkSoundPath)
                    $('#text_modal').modal('toggle')
                    //$('#quiz_body button').unbind('click');
                    queBackUp[c] = {
                        "quiz_body": $("#quiz_body").html()
                    }
                    //{"coAnwser":"--","stAnwser":"--","answerState":true,"answerType":"essay"}
                    score[c] = {
                        "coAnwser": "--",
                        "stAnwser": "--",
                        "answerState": true,
                        "answerType": "essay"
                    }
                });
                
                /*essay  end*/
                break;
            case "trueFalse":
                /*trueFalse start*/
                
                
                $('#true_modal .alert').text(quizData[c].rightfeedback)
                $('#wrong_modal .alert').text(quizData[c].wrongfeedback)
                $('<div class="t_f_quiz"><button type="button" class="text-success" date-state="true"><i class="fa fa-check "></i></button> <button type="button" class="text-danger" date-state="false"><i class="fa fa-times"></i></button></div>').appendTo("#quiz_body").children().bind('click', function (event) {
                    // console.log($(this).index())
                    if ((quizData[c].co_answer && $(this).index() == 0) || (!(quizData[c].co_answer) && $(this).index() == 1)) {
                        // $(this).addClass("alert-success")
                        $('#true_modal').modal('toggle')
                        playAudio(cAnswerSoundPath)
                        $(this).find("i").css("border", "4px solid #3CF200")
                        //{"coAnwser":"--","stAnwser":"--","answerState":true,"answerType":"essay"}
                        score[c] = {
                            "coAnwser": quizData[c].co_answer,
                            "stAnwser": quizData[c].co_answer,
                            "answerState": true,
                            "answerType": "trueFalse"
                        }
                    } else {
                        // $(this).addClass("alert-danger")
                        $('#wrong_modal').modal('toggle')
                        playAudio(wAnswerSoundPath)
                        //console.log($(this).index())
                        //$('.t_f_quiz button').not(this).find("i").css("border", "4px solid #3CF200")
                        $(this).find("i").css("border", "4px solid #EC1F27")
                        score[c] = {
                            "coAnwser": quizData[c].co_answer,
                            "stAnwser": !(quizData[c].co_answer),
                            "answerState": false,
                            "answerType": "trueFalse"
                        }
                    }
                    
                    $('#quiz_body .t_f_quiz button').unbind('click');
                    queBackUp[c] = {
                        "quiz_body": $("#quiz_body").html()
                    }
                });   
                $("#quiz_body").append('<div class="clearfix"></div>')
                /*trueFalse  end*/
                break;
            case "singleChoiceTxt":
                /*singleChoiceTxt start*/
                
                //answerArr this  the array to save all src imgs
                var answerArr = [];
                //wrongAnswerArr data wrong image source 
                var wrongAnswerArr = quizData[c].wro_answer;
                answerArr.push({
                    st: true,
                    src: quizData[c].co_answer
                });
                for (var i = 0; i < wrongAnswerArr.length; i++) {
                    answerArr.push({
                        st: false,
                        src: wrongAnswerArr[i].wrong
                    });
                }
                $('#true_modal .alert').text(quizData[c].rightfeedback)
                $('#wrong_modal .alert').text(quizData[c].wrongfeedback)
                answerArr.sort(function () {
                    return Math.random() - .5
                });
                $.each(answerArr, function (index, value) {
                    if (value.st) {
                        coAnswerClass = "co_answer"
                    }
                    $('<h4 class="text-quiz bg-primary ' + coAnswerClass + '">' + value.src + '</h4>').appendTo("#quiz_body").bind('click', function (event) {
                        
                        if (value.st) {
                            $(this).addClass("alert-success")
                            $('#true_modal').modal('toggle')
                            playAudio(cAnswerSoundPath)
                            //{"coAnwser":"--","stAnwser":"--","answerState":true,"answerType":"essay"}
                            score[c] = {
                                "coAnwser": $(this).index(),
                                "stAnwser": "--",
                                "answerState": true,
                                "answerType": "singleChoiceTxt"
                            }
                        } else {
                            $(this).addClass("alert-danger")
                            $('#wrong_modal').modal('toggle')
                            playAudio(wAnswerSoundPath)
                            score[c] = {
                                "coAnwser": $('#quiz_body h4.co_answer').index(),
                                "stAnwser": $(this).index(),
                                "answerState": false,
                                "answerType": "singleChoiceTxt"
                            }
                        }
                        
                        $('#quiz_body h4').unbind('click');
                        queBackUp[c] = {
                            "quiz_body": $("#quiz_body").html()
                        }
                    });
                    coAnswerClass = "";
                })
                
                $("#quiz_body").append('<div class="clearfix"></div>')
                
                /*singleChoiceTxt  end*/
                break;
            case "singleChoiceImg":
                /*singleChoiceImg*/
                
                //answerArr this  the array to save all src imgs
                var answerArr = [];
                //wrongAnswerArr data wrong image source 
                var wrongAnswerArr = quizData[c].wro_img_src;
                answerArr.push({
                    st: true,
                    src: quizData[c].co_img_src
                });
                for (var i = 0; i < wrongAnswerArr.length; i++) {
                    answerArr.push({
                        st: false,
                        src: wrongAnswerArr[i].wrong
                    });
                    
                }
                $('#true_modal .alert').text(quizData[c].rightfeedback)
                $('#wrong_modal .alert').text(quizData[c].wrongfeedback)
                answerArr.sort(function () {
                    return Math.random() - .5
                });
                $.each(answerArr, function (index, value) {
                    if (value.st) {
                        coAnswerClass = "co_answer"
                    }
                    $('<div class="quiz_check thumbnail ' + coAnswerClass + '" > <label><input type="radio" name="1" class="icheck"/><img src="' + value.src + '" alt=""/> </label> </div><!--end quiz_check-->').appendTo("#quiz_body").find("input").on('ifChecked', function (event) {
                        // alert(event.type + ' callback');
                        if (value.st) {
                            
                            $(this).parent().parent().parent().addClass("true_quiz")
                            $('#true_modal').modal('toggle')
                            playAudio(cAnswerSoundPath)
                            //{"coAnwser":"--","stAnwser":"--","answerState":true,"answerType":"essay"}
                            score[c] = {
                                "coAnwser": $(this).parent().parent().parent().index(),
                                "stAnwser": $(this).parent().parent().parent().index(),
                                "answerState": true,
                                "answerType": "singleChoiceImg"
                            }
                        } else {
                            $(this).parent().parent().parent().addClass("false_quiz")
                            $('#wrong_modal').modal('toggle')
                            playAudio(wAnswerSoundPath)
                            score[c] = {
                                "coAnwser": $('#quiz_body div.co_answer').index(),
                                "stAnwser": $(this).parent().parent().parent().index(),
                                "answerState": false,
                                "answerType": "singleChoiceImg"
                            }
                        }
                        
                        $('input').iCheck('disable');
                        queBackUp[c] = {
                            "quiz_body": $("#quiz_body").html()
                        }
                    });
                    
                })
                coAnswerClass = "";
                $("#quiz_body").append('<div class="clearfix"></div>')
                
                /*singleChoiceImg  end*/
                break;
            case "multiChoiceTxt":
                /*multiChoiceTxt start*/
                var trueAnswerCount = 0;
                //answerArr this  the array to save all src imgs
                var answerArr = [];
                //wrongAnswerArr data wrong image source 
                var wrongAnswerArr = quizData[c].wro_answer;
                for (var i = 0; i < wrongAnswerArr.length; i++) {
                    answerArr.push({
                        st: false,
                        src: wrongAnswerArr[i].wrong
                    })
                }
                //correctAnswer data wrong image source 
                var correctAnswerArr = quizData[c].co_answer;
                for (var i = 0; i < correctAnswerArr.length; i++) {
                    answerArr.push({
                        st: true,
                        src: correctAnswerArr[i].correct
                    })
                    
                }
                answerArr.sort(function () {
                    return Math.random() - .5
                });
                $.each(answerArr, function (index, value) {
                    if (value.st) {
                        coAnswerClass = "co_answer"
                    }
                    $('<h4 class="text-quiz bg-primary ' + coAnswerClass + '">' + value.src + '</h4>').appendTo("#quiz_body").bind('click', function (event) {
                        $(this).toggleClass("btn-info");
                        if (value.st) {
                            if ($(this).hasClass("btn-info")) {
                                trueAnswerCount++
                            } else {
                                trueAnswerCount--
                            }
                        }
                        
                    });
                    coAnswerClass = ""
                })
                
                $('<div class="clearfix"></div><button type="button" id="btn_check" class="btn view_quiz_btn" >تحقق  </button>').appendTo("#quiz_body")
                $('#btn_check').bind("click", function () {
                    
                    //console.log("ddd")
                    $(this).attr("disabled", "disabled")
                    $('#quiz_body h4').unbind('click');
                    $(this).unbind('click');
                    queBackUp[c] = {
                        "quiz_body": $("#quiz_body").html()
                    }
                    var temp = checkDuplicateArr(arrOfIndex('#quiz_body h4.co_answer'), arrOfIndex('#quiz_body h4.btn-info'))
                    if (trueAnswerCount > 0) {
                        if (temp) {
                            $('#true_modal .alert').text(quizData[c].rightfeedback)
                            $('#true_modal').modal('toggle')
                            playAudio(cAnswerSoundPath)
                        } else {
                            $('#wrong_modal .alert').text(quizData[c].partialfeedback)
                            $('#wrong_modal').modal('toggle')
                            playAudio(wAnswerSoundPath)
                        }
                    } else {
                        $('#wrong_modal .alert').text(quizData[c].wrongfeedback)
                        $('#wrong_modal').modal('toggle')
                    }
                    
                    
                    score[c] = {
                        "coAnwser": arrOfIndex('#quiz_body h4.co_answer'),
                        "stAnwser": arrOfIndex('#quiz_body h4.btn-info'),
                        "answerState": temp,
                        "answerType": "multiChoiceTxt"
                    }
                    
                })
                /*multiChoiceTxt  end*/
                break;
            case "multiChoiceImg":
                /*multiChoiceImg start*/
                loadMultiChoiceImg(c)
                /*multiChoiceImg  end*/
                break;
            case "fillBlanK":
                /*fillBlanK start*/
                loadFillBlank(c)
                /*fillBlanK  end*/
                break;
            case "fillDropDown":
                /*fillDropDown start*/
                loadFillDropDown(c)
                /*fillDropDown  end*/
                break;
            case "fillByDragDrop":
                /*fillByDragDrop start*/
                loadFillByDragDrop(c)
                /*fillByDragDrop  end*/
                break;
            case "math":
                /*fillDropDown start*/
                loadMath(c)
                /*fillDropDown  end*/
                break;
        }
    } else {
        $('#q_head').text(quizData[c].header)
        $("#quiz_body").html(queBackUp[c].quiz_body)
        if(quizData[c].type == "math"){
            loadMathQueCheck(c,quizData[c],false);
        }else if(quizData[c].type == "fillBlanK" && quizData[c].example ){
             
            loadFillBlankExample(c)
        }
    }
    // to set report in final question
    if (quizOptions.report) {
        if (c > quizData.length - 2) {
            if (score[c] == undefined) {
                score[c] = undefined
            }
            // console.log(score)
            loadReport()
            
        }
    }
    $('.icheck').iCheck({
        checkboxClass: 'icheckbox_flat-blue',
        radioClass: 'iradio_flat-blue',
        increaseArea: '20%' // optional
    });
    $(".frame_head .frame_refresh").bind("click", function (e) {
        history.go(0);
    })
}

function loadQuizNav(co) {
    //console.log("loadQuizNav")
    $(".quiz_nav .pagination").html('')
    var tempClass = 'class="active"';
    
    
    $(' <li><a href="#">&laquo;</a></li>').appendTo(".quiz_nav .pagination").bind('click', function () {
        if (currQ > 1) {
            playAudio(clickSoundPath)
            currQ = $(".quiz_nav .pagination li.active").index() - 1
            //console.log(currQ)
            loadQuiz(currQ - 1)
            $(".quiz_nav .pagination li").removeClass('active')
            $(".quiz_nav .pagination li").eq(currQ).addClass('active')
        }
    })
    
    for (var i = 0; i < co; i++) {
        temp = i + 1
        
        $('<li ' + tempClass + '><a href="#">' + temp + '</a></li>').appendTo(".quiz_nav .pagination").bind('click', function () {
            playAudio(clickSoundPath)
            currQ = parseInt($(this).index())
            loadQuiz(currQ - 1)
            //console.log($(this).index()+1)
            $(".quiz_nav .pagination li").removeClass('active')
            $(this).addClass('active')
        })
        tempClass = ''
    }
    $('<li><a href="#">&raquo;</a></li>').appendTo(".quiz_nav .pagination").bind('click', function () {
        //          console.log($(".quiz_nav .pagination li").length-2)
        if (currQ < $(".quiz_nav .pagination li").length - 2) {
            playAudio(clickSoundPath)
            currQ = $(".quiz_nav .pagination li.active").index() + 1
            //              console.log(currQ)
            loadQuiz(currQ - 1)
            $(".quiz_nav .pagination li").removeClass('active')
            $(".quiz_nav .pagination li").eq(currQ).addClass('active')
        }
    })
}

function loadQuizItemNav(co) {
    //console.log("loadQuizNav")
    $(".quiz_nav").html('')
      $('.quiz_nav').bootpag({
          total: co,
          page: 1,
          maxVisible: 5,
          leaps: true,
          firstLastUse: true,
          first: '&#8677;',
          last: '&#8676',
          wrapClass: 'pagination',
          activeClass: 'active',
          disabledClass: 'disabled',
          nextClass: 'next',
          prevClass: 'prev',
          lastClass: 'last',
          firstClass: 'first'
      }).on("page", function(event, /* page number here */ num){
           playAudio(clickSoundPath)
           
           loadQuiz(num - 1)
        });
    
}

function loadReport() {
    var checkBoolen = function (a) {
        if (a) {
            return "صحيح"
        } else {
            return "خطأ"
        }
    };
    if($('#quiz_body button').hasClass('quiz_report')){
        $('#quiz_body button.quiz_report').remove()
    }
    $('<button type="button" class="quiz_report pull-right" data-toggle="modal" >عرض التقرير</button><div class="clearfix"></div>').appendTo('#quiz_body').first().bind("click", function () {
        var reportArr = [],totalDegree = 0,finalDegree = 0, cuQuDegree = 0,tempDegree;
        $.each(score, function (index, value) {
            if (value != undefined) {
                switch (value.answerType) {
                    case "essay":
                        cuQuDegree = 0;
                        reportArr[index] = {
                            "coAnwser": "--",
                            "stAnwser": "--",
                            "degree": quiz['report'].essay
                        }
                        break;
                    case "trueFalse":
                        cuQuDegree = quiz['report'].trueFalse;
                        if (value.answerState) {
                            
                            reportArr[index] = {
                                "coAnwser": checkBoolen(value.coAnwser),
                                "stAnwser": checkBoolen(value.coAnwser),
                                "degree": quiz['report'].trueFalse
                            }
                            
                        } else {
                            reportArr[index] = {
                                "coAnwser": checkBoolen(value.coAnwser),
                                "stAnwser": checkBoolen(!(value.coAnwser)),
                                "degree": 0
                            }
                        }
                        
                        break;
                    case "singleChoiceTxt":
                        
                        cuQuDegree = quiz['report'].singleChoiceTxt;
                        
                        if (value.answerState) {
                            reportArr[index] = {
                                "coAnwser": checkChoise(value.coAnwser),
                                "stAnwser": checkChoise(value.coAnwser),
                                "degree": quiz['report'].singleChoiceTxt
                            }
                        }else {
                            reportArr[index] = {
                                "coAnwser": checkChoise(value.coAnwser),
                                "stAnwser": checkChoise(value.stAnwser),
                                "degree": 0
                            }
                        }
                       //  console.log("quDegree")
                       // console.log(cuQuDegree)
                       // console.log(quiz['report'].singleChoiceTxt)
                        break;
                    case "singleChoiceImg":
                        cuQuDegree = quiz['report'].singleChoiceImg;
                        if (value.answerState) {
                            reportArr[index] = {
                                "coAnwser": checkChoise(value.coAnwser),
                                "stAnwser": checkChoise(value.coAnwser),
                                "degree": quiz['report'].singleChoiceTxt
                            }
                        } else {
                            reportArr[index] = {
                                "coAnwser": checkChoise(value.coAnwser),
                                "stAnwser": checkChoise(value.stAnwser),
                                "degree": 0
                            }
                        }
                        break;
                    case "multiChoiceTxt":
                        cuQuDegree = quiz['report'].multiChoiceTxt;
                        if (value.answerState) {
                            reportArr[index] = {
                                "coAnwser": checkChoise(value.coAnwser),
                                "stAnwser": checkChoise(value.stAnwser),
                                "degree": quiz['report'].multiChoiceTxt
                            }
                        } else {
                            var quDegree = quiz['report'].multiChoiceTxt / value.coAnwser.length
                            var quSum = 0;
                            // console.log(quDegree)
                            $.each(value.stAnwser, function (iAnswer, vAnswer) {
                                
                                if (jQuery.inArray(vAnswer, value.coAnwser) != -1) {
                                    quSum += quDegree
                                    //console.log(vAnswer)
                                } else {
                                    quSum -= quDegree
                                }
                            })
                            for (var i = value.coAnwser.length; i < value.stAnwser; i++) {
                                quSum -= quDegree;
                            }
                            if(quSum<0){quSum=0}
                            reportArr[index] = {
                                "coAnwser": checkChoise(value.coAnwser),
                                "stAnwser": checkChoise(value.stAnwser),
                                "degree": quSum.toFixed(1)
                            }
                        }
                        break;
                    case "multiChoiceImg":
                        cuQuDegree = quiz['report'].multiChoiceImg;
                        if (value.answerState) {
                            reportArr[index] = {
                                "coAnwser": checkChoise(value.coAnwser),
                                "stAnwser": checkChoise(value.stAnwser),
                                "degree": quiz['report'].multiChoiceImg
                            }
                        } else {
                            var quDegree = quiz['report'].multiChoiceImg / value.coAnwser.length
                            var quSum = 0;
                            // console.log(quDegree)
                            $.each(value.stAnwser, function (iAnswer, vAnswer) {
                                
                                if (jQuery.inArray(vAnswer, value.coAnwser) != -1) {
                                    quSum += quDegree
                                    //console.log(vAnswer)
                                } else {
                                    quSum -= quDegree
                                }
                            })
                            for (var i = value.coAnwser.length; i < value.stAnwser; i++) {
                                quSum -= quDegree;
                            }
                            if(quSum<0){quSum=0}
                            reportArr[index] = {
                                "coAnwser": checkChoise(value.coAnwser),
                                "stAnwser": checkChoise(value.stAnwser),
                                "degree": quSum.toFixed(1)
                            }
                        }
                        break;        
                    case "fillBlanK":
                        //start fillBlank report
                        cuQuDegree = quiz['report'].fillBlanK;
                        if (value.answerState) {
                            reportArr[index] = {
                                "coAnwser": value.coAnwser.toString(),
                                "stAnwser": value.stAnwser.toString(),
                                "degree": quiz['report'].fillBlanK
                            }
                        } else {
                            var quDegree = quiz['report'].fillBlanK / value.coAnwser.length
                            var quSum = 0;
                            
                            quSum = quDegree * value.coAnswerCount
                            reportArr[index] = {
                                "coAnwser": value.coAnwser.toString(),
                                "stAnwser": value.stAnwser.toString(),
                                "degree": quSum.toFixed(1)
                            }
                        }
                        
                        //end fillBlank report
                        break;
                    case "fillDropDown":
                        //start fillBlank report
                        
                   
                        cuQuDegree = quiz['report'].fillDropDown; 
                        console.log(cuQuDegree)
                        //console.log(value.answerState)
                        if (value.answerState) {
                            reportArr[index] = {
                                "coAnwser": value.coAnwser.toString(),
                                "stAnwser": value.stAnwser.toString(),
                                "degree": quiz['report'].fillDropDown
                            }
                        } else {
                            var quDegree = quiz['report'].fillDropDown / value.coAnwser.length
                            var quSum = 0;
                            
                            quSum = quDegree * value.coAnswerCount
                            reportArr[index] = {
                                "coAnwser": value.coAnwser.toString(),
                                "stAnwser": value.stAnwser.toString(),
                                "degree": quSum.toFixed(1)
                            }
                        }
                        
                        //end fillBlank report
                        break; 
                    case "fillByDragDrop":
                        //start fillBlank report
                        
                        //console.log("quDegree")
                        //console.log(value.answerState)
                        cuQuDegree = quiz['report'].fillDropDown;
                        if (value.answerState) {
                            reportArr[index] = {
                                "coAnwser": value.coAnwser.toString(),
                                "stAnwser": value.stAnwser.toString(),
                                "degree": quiz['report'].fillDropDown
                            }
                        } else {
                            var quDegree = quiz['report'].fillDropDown / value.coAnwser.length
                            var quSum = 0;
                            
                            quSum = quDegree * value.coAnswerCount
                            reportArr[index] = {
                                "coAnwser": value.coAnwser.toString(),
                                "stAnwser": value.stAnwser.toString(),
                                "degree": quSum.toFixed(1)
                            }
                        }
                        
                        //end fillBlank report
                        break; 
                    case "math":
                        
                         if (value.answerState) {
                            reportArr[index] = {
                                "coAnwser": value.coAnwser,
                                "stAnwser": value.stAnwser,
                                "degree": quiz['report'].math
                            }
                        } else {
                            reportArr[index] = {
                                "coAnwser": value.coAnwser,
                                "stAnwser": value.stAnwser,
                                "degree": 0
                            }
                        }
//                        console.log("reportArr[index].degree")
//                        console.log(reportArr[index].degree)
                        break;      
                }
            } else {
                /*this code when user skip question without answer*/
                reportArr[index] = {
                    "coAnwser": "--",
                    "stAnwser": "--",
                    "degree": "لم يتم الحل "
                }
                switch (quizData[index].type) {
                    case "essay":
                        cuQuDegree = 0;
                        break;
                    case "trueFalse":
                        cuQuDegree = quiz['report'].trueFalse;
                        break;
                    case "singleChoiceTxt":
                        cuQuDegree = quiz['report'].singleChoiceTxt;
                        break;
                    case "singleChoiceImg":
                        cuQuDegree = quiz['report'].singleChoiceImg;
                        break;
                    case "multiChoiceTxt":
                        cuQuDegree = quiz['report'].multiChoiceTxt;
                        break;
                    case "multiChoiceImg":
                        cuQuDegree = quiz['report'].multiChoiceImg;
                        break;
                    case "fillBlanK":
                        cuQuDegree = quiz['report'].fillBlanK;
                        break;
                    case "fillDropDown":
                        cuQuDegree = quiz['report'].fillDropDown;
                        break;
                    case "fillByDragDrop":
                        cuQuDegree = quiz['report'].fillByDragDrop;
                        break;        
                case "math":
                        cuQuDegree = quiz['report'].math;
                        break;
                }
            }
            if ($.isNumeric(reportArr[index].degree)) {
                finalDegree += parseInt(reportArr[index].degree);
                
            }
            totalDegree += cuQuDegree
//             console.log("quDegree")
//                        console.log(finalDegree)
//                        console.log(quiz['report'].singleChoiceTxt)
        })
        $("#report_modal .modal-title").text(quiz['report'].header)
        $("#report_modal .modal-body tfoot td:nth-child(1)").text("  الاجمالي   ( الأسئلة التى لها درجات ) ")
        $("#report_modal .modal-body tfoot td:nth-child(2)").text(totalDegree + " / " + parseInt(finalDegree))
        $("#report_modal .modal-body tbody").html('');
        $.each(reportArr, function (index, value) {
            if (value != undefined) {
                var valueD=value.degree
                if (valueD==0){
                    valueD=0
                }
                $("#report_modal .modal-body tbody").append('<tr><td>' + parseInt(index + 1) + '</td><td>' + value.stAnwser + '</td><td>' + value.coAnwser + '</td><td>' + valueD + '</td></tr>')
            }
        })
        $('#report_modal').modal('toggle')
        
    })
    
}

function loadMultiChoiceImg(c) {
    var coAnswerClass = "";
    var trueAnswerCount = 0;  
    var answerArr = [];//answerArr this  the array to save all src imgs
    var wrongAnswerArr = quizData[c].wro_img_src; //wrongAnswerArr data wrong image source 
    for (var i = 0; i < wrongAnswerArr.length; i++) {
        answerArr.push({
            st: false,
            src: wrongAnswerArr[i].wrong
        })
    }
    var correctAnswerArr = quizData[c].co_img_src; //correctAnswer data wrong image source 
    for (var i = 0; i < correctAnswerArr.length; i++) {
        answerArr.push({
            st: true,
            src: correctAnswerArr[i].correct
        })
        
    }
    answerArr.sort(function () {
        return Math.random() - .5
    });
    $.each(answerArr, function (index, value) {
        if (value.st) {
            coAnswerClass = "co_answer"
        }
        $('<div class="quiz_check thumbnail ' + coAnswerClass + '" > <label><input type="checkbox" name="1" class="icheck"/><img src="' + value.src + '" alt=""/> </label> </div><!--end quiz_check-->').appendTo("#quiz_body").find("input");
        coAnswerClass = ""
    })
    
    $('<div class="clearfix"></div><button type="button" id="btn_check" class="btn view_quiz_btn" >تحقق  </button>').appendTo("#quiz_body")
    $('#btn_check').bind("click", function () {
        //console.log($('#quiz_body .quiz_check label div.checked').parent().parent().index())
        $('#quiz_body .quiz_check label div.checked').parent().parent().addClass("st_answer")
        
        //console.log("ddd")
        $(this).attr("disabled", "disabled")
        $('input').iCheck('disable');
        $(this).unbind('click');
        queBackUp[c] = {
            "quiz_body": $("#quiz_body").html()
        }
        if ($('#quiz_body div.st_answer').hasClass("co_answer")) {
            trueAnswerCount++
        }
        var temp = checkDuplicateArr(arrOfIndex('#quiz_body div.co_answer'), arrOfIndex($('#quiz_body div.st_answer')))
        // console.log(temp)
        if (trueAnswerCount > 0) {
            if (temp) {
                $('#true_modal .alert').text(quizData[c].rightfeedback)
                $('#true_modal').modal('toggle')
                playAudio(cAnswerSoundPath)
            } else {
                $('#wrong_modal .alert').text(quizData[c].partialfeedback)
                $('#wrong_modal').modal('toggle')
                playAudio(wAnswerSoundPath)
            }
        } else {
            $('#wrong_modal .alert').text(quizData[c].wrongfeedback)
            $('#wrong_modal').modal('toggle')
            playAudio(wAnswerSoundPath)
        }
        
        
        score[c] = {
            "coAnwser": arrOfIndex('#quiz_body div.co_answer'),
            "stAnwser": arrOfIndex($('#quiz_body .quiz_check label div.checked').parent().parent()),
            "answerState": temp,
            "answerType": "multiChoiceImg"
        }
        
    })
}

function loadFillBlank(c) {
    var trueAnswerCount = 0,stAnswerArr = [],coAnswerArr = [];
    //question Words Arr this  the array to save all src imgs
    var queBody = "",queWordsArr = quizData[c].body.split(" ");
    //console.log(queWordsArr)
    var tempWord='';
    $.each(queWordsArr, function (index, value) {
        tempWord=value;
        if (value == "=====") {
            tempWord = '<input class="quiz_blank" type="text" name="FirstName" placeholder="..............................">'
        }
        queBody += tempWord + " "
    })
    $("#quiz_body").html('')
    $('<h1 class="quiz_title">' + queBody + '</h1>').appendTo("#quiz_body").css({
        "font-size": "20px",
        "line-height": 1.5
    })
    //$('<input>').appendTo("#quiz_body")
    $('<div class="clearfix"></div><button type="button" id="btn_check" class="btn view_quiz_btn" >تحقق  </button>').appendTo("#quiz_body")

    $('#btn_check').bind("click", function () {
        if( score[c] == undefined){
        $.each($('#quiz_body input.quiz_blank'), function (index, value) {
            
            if ($.trim(value.value) == $.trim(quizData[c].blankwords[index].correct)) {
                trueAnswerCount++
            }
            coAnswerArr.push($.trim(quizData[c].blankwords[index].correct))
            if ($.trim(value.value) != "") {
                stAnswerArr.push($.trim(value.value))
            } else {
                stAnswerArr.push("--")
            }
        })
        //          console.log(trueAnswerCount)
        }
        
        $('input').attr("disabled", "disabled");
        var temp = (function () {
            if (trueAnswerCount == $('#quiz_body input.quiz_blank').length) {
                return true
            } else {
                return false
            }
        })
        //convert fillblank to example if example tag in json =true 
        if(!(quizData[c].example)){
            $(this).attr("disabled", "disabled")
            $(this).unbind('click');
            if (trueAnswerCount > 0) {
                if (temp()) {
                    $('#true_modal .alert').text(quizData[c].rightfeedback)
                    $('#true_modal').modal('toggle')
                    playAudio(cAnswerSoundPath)
                } else {
                    $('#wrong_modal .alert').text(quizData[c].partialfeedback)
                    $('#wrong_modal').modal('toggle')
                    playAudio(wAnswerSoundPath)
                }
            } else {
                $('#wrong_modal .alert').text(quizData[c].wrongfeedback)
                $('#wrong_modal').modal('toggle')
                playAudio(wAnswerSoundPath)
            }
        }else{
           loadFillBlankExample(c) 
        }
        if( score[c] == undefined){
           
            queBackUp[c] = { "quiz_body": $("#quiz_body").html() }
            //console.log(coAnswerArr)
            score[c] = {
                "coAnwser": coAnswerArr,
                "stAnwser": stAnswerArr,
                "answerState": temp(),
                "answerType": "fillBlanK",
                "coAnswerCount": trueAnswerCount
            }
            
        }
        
        
    })
}
function loadFillBlankExample(c){
     var flag = 0,ansBody='',queWordsArr = quizData[c].body.split(" ");
            tempWord='';
            $.each(queWordsArr, function (index, value) {
                tempWord=value;
                if (value == "=====") {
                    tempWord = '<span class="text-success">'+quizData[c].blankwords[flag].correct+'</span>'
                    flag++
                }
                ansBody += tempWord + " "
            })
            $('#preview_location').html(ansBody)
                 $('#myModalLabel').text("الإجابة")
                 $('#previewmodal').modal('toggle')
}
function loadFillDropDown(c) {
    var trueAnswerCount = 0,stAnswerArr = [],coAnswerArr = [];
    var queBody = "",queWordsArr = quizData[c].body.split(" ");      //question Words Arr 
    
    var flag = 0,blankwordsArr=[];
    
    
    for (var i = 0; i < quizData[c].blankwords.length; i++) {
        
        var arr=[]
        arr.push(quizData[c].blankwords[i].correct)
        coAnswerArr.push($.trim(quizData[c].blankwords[i].correct))
        for(var j=0;j<quizData[c].blankwords[i].wrong.length;j++){
            arr.push(quizData[c].blankwords[i].wrong[j])
        }
        arr.sort(function () {
            return Math.random() - .5
        });
        blankwordsArr[i]=arr  
    }
    //             console.log(coAnswerArr)
    
    
    $.each(queWordsArr, function (index, value) {
        if (value == "=====") {
            queWordsArr[index]='<select class="selectpicker"  title="اختر الاجابة المناسبة"  data-style="btn-info"  >';
            
            for(var i=0;i<blankwordsArr[flag].length;i++){
                //console.log(quizData[c].blankwords[flag].wrong)
                queWordsArr[index]+='<option>'+blankwordsArr[flag][i]+'</option>';
            }
            
            queWordsArr[index]+='</select>';
            
            // queWordsArr[index] = '<input class="quiz_blank" type="text" name="FirstName" placeholder="..............................">'
            //quizData[c].blankwords[flag].correct
            flag++
        }
        queBody += queWordsArr[index] + " "
    })
    $("#quiz_body").html('')
    $('<h1 class="quiz_title">' + queBody + '</h1>').appendTo("#quiz_body").css({
        "font-size": "20px",
        "line-height": 1.5
    })
    //$('<input>').appendTo("#quiz_body")
    $('<div class="clearfix"></div><button type="button" id="btn_check" class="btn view_quiz_btn" >تحقق  </button>').appendTo("#quiz_body")
    $('#btn_check').bind("click", function () {
        
        $.each($('#quiz_body .selectpicker'), function (index, value) {
            
            if ($.trim(value.value) == $.trim(coAnswerArr[index])) {
                trueAnswerCount++
            }
            
            if ($.trim(value.value) != "") {
                stAnswerArr.push($.trim(value.value))
            } else {
                stAnswerArr.push("--")
            }
        })
        //              console.log(trueAnswerCount)
        //              console.log(stAnswerArr)
        //              console.log(coAnswerArr)
        
        $(this).attr("disabled", "disabled")
        $('.selectpicker').attr("disabled", "disabled");
        $(this).unbind('click');
        queBackUp[c] = {
            "quiz_body": $("#quiz_body").html()
        }
        var temp = (function () {
            if (trueAnswerCount == coAnswerArr.length) {
                return true
            } else {
                return false
            }
        })
        
        if (trueAnswerCount > 0) {
            if (temp()) {
                $('#true_modal .alert').text(quizData[c].rightfeedback)
                $('#true_modal').modal('toggle')
                playAudio(cAnswerSoundPath)
            } else {
                $('#wrong_modal .alert').text(quizData[c].partialfeedback)
                $('#wrong_modal').modal('toggle')
                playAudio(wAnswerSoundPath)
            }
        } else {
            $('#wrong_modal .alert').text(quizData[c].wrongfeedback)
            $('#wrong_modal').modal('toggle')
            playAudio(wAnswerSoundPath)
        }
        //          console.log(coAnswerArr)
        score[c] = {
            "coAnwser": coAnswerArr,
            "stAnwser": stAnswerArr,
            "answerState": temp(),
            "answerType": "fillDropDown",
            "coAnswerCount": trueAnswerCount
        }
        
    })
}
function loadFillByDragDrop(c){
     var trueAnswerCount = 0,stAnswerArr = [],allAnswerArr = [],questionsArr=[],queBody="";
    for (var i = 0; i < quizData[c].question.length; i++) {
        var arr=[]
        arr[0]=quizData[c].question[i].body.split(" ");
        arr[1]=quizData[c].question[i].answer  
        allAnswerArr.push(quizData[c].question[i].answer)
       questionsArr.push(arr)
        }
    for (var i = 0; i < quizData[c].wrong.length; i++) {
         allAnswerArr.push(quizData[c].wrong[i])
        }
    
    allAnswerArr.sort(function () {return Math.random() - .5});
    $.each(allAnswerArr,function(index,value){
        queBody+='<li>'+value+'</li>';
    })
    
    $("#quiz_body").html('')
    $('<h1 class="quiz_title"><ul class="list-inline answer_drag">'+queBody+'</ul></h1>').appendTo("#quiz_body").css({
        "font-size": "20px",
        "line-height": 1.5
    })
    $('<ol class="text-info" style="list-style: decimal inside;"></ol>').appendTo("#quiz_body").css({
        "font-size": "20px",
        "line-height": 1.6
    })
    $.each(questionsArr,function(indexA,valueA){
//         console.log(valueA[0][0])
         var str=""
         for(var i = 0; i < valueA[0].length; i++) {
             if (valueA[0][i] == "=====") {
                  str+='<span class="answer_drop"> .................. <span>'//" فراغ  "
             }else{
              str+=valueA[0][i]+" "    
             }
           
         }
        $('<li>'+str+'</li>').appendTo("#quiz_body ol")
    })
    
    $("#quiz_body .answer_drag li").draggable({
        containment: "#quiz_body",
         cursor: "move"
        
    })
    $("#quiz_body ol li .answer_drop").droppable({
  accept: ".special"
});
   // console.log("==========")
   // console.log(questionsArr)
    
    // console.log(allAnswerArr)
     // console.log()
    
    
    
    
    
}
function loadMath(c){
    $('.frame_head').show()
    $("#quiz_body").html('<section class="site_contents"><div class="container"><div class="row"><div class="que_body"></div></div><!--end row--></div><!--end container--></section> <div class="footer_container"><div class="que_controls"><input class="math_blank text-info" type="text" name="FirstName" placeholder="اجب هنا"><button type="button" id="btn_check" class="btn view_math_btn" >تحقق  </button> <button type="button" id="btn_view" disabled class="btn view_math_btn" >عرض الاجابة  </button><!--<button type="button" id="btn_next"  class="btn btn-danger" >التالي<i class="fa fa-arrow-left"></i>   </button>--></div><div class="clearfix"></div></div>')
    /*if(mathData.length<1){
        $('#btn_next').hide()
    }*/
    var queData=quizData[c]
//    console.log(queData.header)
    $('<h2 class="text-danger math_title"><span class="fa fa-question bg-info"> </span>   '+queData.header+'</h2>').appendTo(".que_header")
    loadMathQueBody(c,queData)
    if(queData.narration!=undefined){
       playAudio(queData.narration)}
    // if this question for test or eqplain to test false
    if(queData.example){
        $("#btn_view").text("فكر ثم اعرض الاجابة" )
        loadMathQueCheck(c,queData,false);
        $('#btn_check').hide();
         $('input.math_blank').hide();
        
    }
}
function loadMathQueBody(c,queData){
    switch(queData.body.type){
        case "text": 
            $('<h3 class="text-primary">'+queData.body.text+'</h3>').appendTo(".que_body") 
            break;
        case "image":
            $('<img src="'+queData.body.img+'" id="imagepreview" style="width: 567px; height: 264px;" >').appendTo('.que_body')
            break;
        case "textAndImage":
            $('<img src="'+queData.body.img+'" class="image_text"  ><div class="text_image">'+queData.body.text+'</div> <div class="clearfix"></div>').appendTo('.que_body')
            break;
        case "html":
             $(queData.body.html).appendTo(".que_body") 
            break;
    }
    if(queData.example){
        $("#btn_view").text("فكر ثم اعرض الاجابة" )
        loadMathQueCheck(c,queData,false);
        $('#btn_check').hide();
         $('input.math_blank').hide();
        
    }
    if(!(queData.example)){
         $("#btn_check").bind("click",function(){ 
             playAudio(checkSoundPath)
             if($.trim($("input.math_blank").val())!=""){
                 $(this).attr("disabled", "disabled")
                 $('.math_blank').attr("disabled", "disabled")
                 loadMathQueCheck(c,queData,true);
             }else{
                 $('.math_blank').attr("placeholder", "من فضلك ادخل الاجابة").css({ 
                     "transition": "box-shadow 1s",
                     "box-shadow": "0 0 4px red"
            })
             }
        
        
         })
    }
    
}
function loadMathQueCheck(c,queData,check){
    temp=false;
    if(check){
    if($.trim(queData.answer.correct)==$.trim($("input.math_blank").val())){
            $('#true_modal .alert').text(queData.rightfeedback)
            $('#true_modal').modal('toggle')
            temp=true;
         playAudio(cAnswerSoundPath)
         }
        else{ 
            $('#wrong_modal .alert').text(queData.wrongfeedback)
            $('#wrong_modal').modal('toggle')
            temp=false;
            playAudio(wAnswerSoundPath)
        }}
    
    $('#myModalLabel').text(queData.answer.title)
    
    switch(queData.answer.type){
        case "text": 
            $("#preview_location").html('<h3 class="text-primary">'+queData.answer.text+'</h3>')
            break;
         case "image":
            $('#preview_location').html('<img src="'+queData.answer.img+'" id="imagepreview" style="width: 567px; height: 264px;" >')
            
            break;
        case "textAndImage":
            $('#preview_location').html('<img src="'+queData.answer.img+'" id="imagepreview" style="width: 300px; height: 264px; float: left;" ><div style="width: 167px; height: 264px; float: right;">'+queData.answer.text+'</div> <div class="clearfix"></div>')
            break;
        case "html":
            $('#preview_location').html(queData.answer.html)
            break;
    }
      
 
    $("#btn_view").prop( "disabled", false ).bind("click",function(){
         playAudio(checkSoundPath)
             // here asign the image to the modal when the user click the enlarge link
   $('#previewmodal').modal('show'); // imagemodal is the id attribute assigned to the bootstrap modal, then i use the show function
       })
       
    queBackUp[c] = {
        "quiz_body": $("#quiz_body").html()
    }
    score[c] = {
        "coAnwser": queData.answer.text,
        "stAnwser": $("input.math_blank").val(),
        "answerState": temp,
        "answerType": "math"
    }
    
}

//loadFillBlankExample(c)
   //      queBackUp[c] = { "quiz_body": $("#quiz_body").html() }