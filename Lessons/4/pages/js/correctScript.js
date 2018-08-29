var result;
  var i=0;
$(function(){

     $('.close_msg').click(function() {
        $(".notification_msg").remove();
     });
     //read from dynamic script 
     var jsonFile = sessionStorage.getItem('jsonFile');
     var lesson_id=  sessionStorage.getItem('lessonId');
   
     loadScript("../Data/Lessons/"+lesson_id+"/"+jsonFile+".js",function (a, b) {}).onload = function(){
         $(data).each(function(i,json){ 
             if(json.title != undefined  && json.inside_title != undefined && json.title != undefined ){
                    $('#title').append('<h4>' + json.title + '</h4><hr/><h5>'+json.inside_title+'</h5>');  
                     $('#lesson_title').append(json.pageHeader);
                }   
                $("#noti_msg").append(json.prompt);
                var queBody = "",queWordsArr = json.content.split(" ");
                var tempWord='';
                $.each(queWordsArr, function (index, value) {
                   
                    tempWord=value;
                    if (value == "=====") {
                        //debugger;
                        tempWord = '<input type="text" id="'+i+'" value="'+json.wrongWords[i]+'" onclick="replaceLabel(this)"/>';
                        
                        i++;
                    }
                    queBody += tempWord + " ";
                });
             
           
             
             $("#content").html(queBody);
             //make input take width of value
              $('input').each(function(){
                var value = $(this).val();
                var size  = value.length;
                
                // playing css width
                size = size*2; // average width of a char
                $(this).css('width',size*3);
                
            });
              $('<div class="clearfix"></div><button type="button" id="btn_check" class="btn view_quiz_btn" data-toggle="modal" data-target="#myModal">تحقق  </button>').appendTo("#content");
            });
         
           $('#btn_check').on('click',function() {
               result=0;
               var answers=[];
              $("#content input[type=text]").each(function() {
                 
                  answers.push($(this).val());  
              });
               //compare two arrayes
               $.each( answers, function( key, value ) {
                  // debugger;
                 if( answers[key] == data[0].correctWords[key] ) {
                     $("#"+key).css("color","green");
                     result++;
                   
                 }
                 else{
                     $("#"+key).css("color","red");
                 }
              });
               if(result ==  data[0].correctWords.length)
                            {
                              $(".modal-body").empty().append("<p class='alert alert-success'>أحسنت .. إجابة صحيحة</p>");
                            }
                         else
                             {
                                  $(".modal-body").empty().append("<p class='alert alert-danger'>لقد أجبت "+result+" من "+data[0].correctWords.length+"</p><table class='table'><thead><tr><th>الاجابات الصحيحه</th></tr></thead><tbody></tbody></table>");
                                 
                                 $(data[0].correctWords).each(function(i,v){
                                     $('tbody').append('<tr><td>'+(Number(i)+1)+'</td><td>'+v+'</td></tr>')
                                 });
                             }
 
           });
         
     }
        
        
          
  
});//close of on load

function replaceLabel(element)
{
    //debugger;
    var id = ($(element).attr("id"));
    $("#"+id).val('').css("text-decoration","none").css("width","150");
   

}


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


