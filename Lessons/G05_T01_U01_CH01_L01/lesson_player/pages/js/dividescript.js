$(function(){
       

   
 //read from dynamic script 
    var jsonFile = sessionStorage.getItem('jsonFile');
    var lesson_id=  sessionStorage.getItem('lessonId');
   
         loadScript("../Data/Lessons/"+lesson_id+"/"+jsonFile+".js",function (a, b) {}).onload = function ()
        {
           var MyObject = JSON.parse(data);
          var equation="";
           var question= $("#questiondiv");
            var check,total;
           
        $(MyObject).each(function(i,json){  
             
               $(json.equation).each(function(i,eq){  
                        
                                
                    //print question 
                       if(i %2==0)
                        {  
                            question.append('<div class="item">');
                            $(eq.num).each(function(index,num)
                            {
                                
                            if(num[0] == "*")
                                {
                                   $("#questiondiv .item:last").append('<span class="inputfield"><input type="text" placeholder="-----" onkeypress="return isNumberKey(event)" id="'+num.substring(1)+'" /></span>');
                                    
                                    if(index == 0){  $("#questiondiv .item:last").append("<hr>")}
                                }
                            else
                                {
                                    $("#questiondiv .item:last").append('<span class="staticfield" >'+num+'</span>');
                                    if(index == 0){  $("#questiondiv .item:last").append("<hr>")}
                                }
                                
                                
                                
                                })
                             question.append('</div>');
                        }
                         else
                         {
                             if(eq.op != undefined )
                                question.append('<div class="operation"><span>'+ eq.op +'</span></div>');
                            else
                            {
                                
                             question.append('<div class="operation"><span> = </span></div>');
                             question.append('<div class="item">');
                                
                                 $(eq.result).each(function(index,num)
                            {
                                if(num[0] == "*")
                                {
                             $("#questiondiv .item:last").append('<span class="inputfield"><input type="text" placeholder="-----"  onkeypress="return isNumberKey(event)"  id="'+num.substring(1)+'"  /></span>');
                                    
                                    if(index == 0){  $("#questiondiv .item:last").append("<hr>")}
                             
                            } else
                                {
                                     $("#questiondiv .item:last").append('<span class="staticfield"  >'+num+'</span>');
                                    if(index == 0){  $("#questiondiv .item:last").append("<hr>")}
                                    
                                }
                                     
                                 });
                         
                              question.append('</div>');
                            }}
                         

                      });
       
            
        
                });
       
           
              //correct answer
             $('#checkAnswer').click(function(){
                 
                 total=0;
                 
            $("input[type='text']").each(function(index,answer){
              
                            if(answer.id == answer.value)
                               {
                                   total++;
                     
                               }
                            else
                                {
                                    
                                    $(this).focus()
                                    $(this).parent().addClass("wrong").delay("800").queue(function(nxt) {
                                    $(this).removeClass("wrong");
                                        nxt();
                                    });

                                 
                                }
            });
                
                 
              if(total == $("input[type='text']").length)
                            {
                              $(".modal-body").empty().append("<p class='alert alert-success'>أحسنت .. إجابة صحيحة</p>");
                            }
                         else
                             {
                                  $(".modal-body").empty().append("<p  class='alert alert-danger'>حـاول مرة أخرى</p>");
                             }
                             
                      })
         }
         
});
     function isNumberKey(evt)
      {
         var charCode = (evt.which) ? evt.which : event.keyCode
         if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;

         return true;
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
