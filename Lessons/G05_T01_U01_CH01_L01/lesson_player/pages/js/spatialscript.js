   
$(function(){
    
    
    
$('.close_msg').click(function() {
        $(".notification_msg").remove();
    });
    
     //read from dynamic script 
    var jsonFile = sessionStorage.getItem('jsonFile');
    var lesson_id=  sessionStorage.getItem('lessonId');
   
         loadScript("../Data/Lessons/"+lesson_id+"/"+jsonFile+".js",function (a, b) {}).onload = function ()
        {
    //read json file
        MyObject = JSON.parse(data);
        questionNumber=0,correct_answer=0;
         
       if(MyObject.title != undefined  && MyObject.summary != undefined)
                     $('#title').append('<h4>' + MyObject.title + '</h4><hr/><h5>'+MyObject.summary+'</h5>');
                        
    Drawquestion(questionNumber);
    
    // if select answer ..
    // use delegate to apply event on dynamic choice div ..
    $("#choices").delegate(".choice", "click", function(){
       
        //catch selected div and remove class from other 
            $(".choice").each(function(i,item)
            {
              $(this).removeClass("selected");     
            })
                              
             $(this).addClass("selected");
            
            
        });
    
    //when click next btn to get next question
    $(".w3-teal").click(function(){
        
        // check if user answer or not .
        if($(".selected").length == 0)
            {
                 // catch correct answer
            $(".choice:contains('"+MyObject.questions[questionNumber].correctChoise+"')").addClass("correct");
           //show popup of result ..
             $("#result_here").empty().append("<p class='alert alert-warning'>لم تقم بالإجابة ! </p>");
            
             $('#myModal').modal('show'); 
            }
        //check answer is correct or not
        else if( $(".selected").text() == MyObject.questions[questionNumber].correctChoise )
        {
            //show popup of result ..
            correct_answer++;
             $("#result_here").empty().append("<p class='alert alert-success'>أحسنت .. إجابة صحيحة</p>");
            
             $('#myModal').modal('show'); 
            
        }
        else{
            // catch correct answer
            $(".choice:contains('"+MyObject.questions[questionNumber].correctChoise+"')").addClass("correct");
           //show popup of result ..
             $("#result_here").empty().append("<p class='alert alert-danger'>إجابة خاطئة</p>");
           
             $('#myModal').modal('show'); 
                   
        }
  
        // next question
        if(questionNumber == MyObject.questions.length-1 )
            {
                //show result
                  //show popup of result ..
             $("#result_here").empty().append("<p class='alert alert-success'>لقد أجبت "+correct_answer+" من "+MyObject.questions.length+" </p>").append('<button type="button" class="btn btn-primary" data-dismiss="modal">إغلاق</button>');
                
                $(".modal-header").prepend('<button type="button" class="close" data-dismiss="modal">&times;</button>');
            
             $('#myModal').modal('show'); 
                
             
            }
        else
            {
                questionNumber++;
                //wait to draw next question
                
                setTimeout(function(){Drawquestion(questionNumber)},3000);
                
                // hide pop up when next question showen .. 
                setTimeout(function(){$('#myModal').modal('hide')} ,3000);
            }
    
    });
             
         }
    
});
    

function Drawquestion(questionNumber)
{
    
    $(MyObject.questions[questionNumber]).each(function(i,json){  
            
        $("#choices").empty();
        $("svg").empty();
        $(json.choises).each(function(index,item){
            $("#choices").append('<div class="choice">'+item+'</div>');
           
    });
       // $("#ones").prepend('<svg height="300" width="140"><rect width="5" height="300" x="65" y="0" style="fill:#9999ff;stroke-width:2;stroke:rgb(0,0,0)" /></svg> ');
        
         // $("#ones").prepend('<svg height="300" width="140"></svg> ');
        if(Number(json.ones)>0)
            {
                   var rect = d3.select("#ones svg").append("rect")
                                    .attr("width", "5")
                                     .attr("height", "300")
                                    .attr("style", "fill:#9999ff;stroke-width:2;stroke:rgb(0,0,0)" )
                                    .attr("x","65")
                                    .attr("y","0");
              
                 var startPosition=312;
                for(var i=0; i<Number(json.ones);i++)
                {
                    startPosition-=30;
                    //$("#ones svg").append('  <circle cx="67" cy="'+ startPosition+'" r="13" stroke="#ff33cc" stroke-//width="1" fill="#cc3399" />');
                    var circle = d3.select("#ones svg").append("circle")
                                    .attr("r", "13")
                                    .attr("style", "fill:#ebadd6;stroke:#cc3399;stroke-width:1")
                                    .attr("cx","67")
                                    .attr("cy",startPosition);
                
                }
            }
       
       if(Number(json.Scores)>0)
            {
                   var rect = d3.select("#scores svg").append("rect")
                                    .attr("width", "5")
                                     .attr("height", "300")
                                    .attr("style", "fill:#9999ff;stroke-width:2;stroke:rgb(0,0,0)" )
                                    .attr("x","65")
                                    .attr("y","0");
              
                 var startPosition=312;
                for(var i=0; i<Number(json.Scores);i++)
                {
                    startPosition-=30;
                    //$("#ones svg").append('  <circle cx="67" cy="'+ startPosition+'" r="13" stroke="#ff33cc" stroke-//width="1" fill="#cc3399" />');
                    var circle = d3.select("#scores svg").append("circle")
                                    .attr("r", "13")
                                    .attr("style", "fill:#bf80ff;stroke:#5900b3;stroke-width:1")
                                    .attr("cx","67")
                                    .attr("cy",startPosition);
                
                }
            }
       
        if(Number(json.hundreds)>0)
            {
                   var rect = d3.select("#hundreds svg").append("rect")
                                    .attr("width", "5")
                                     .attr("height", "300")
                                    .attr("style", "fill:#9999ff;stroke-width:2;stroke:rgb(0,0,0)" )
                                    .attr("x","65")
                                    .attr("y","0");
              
                 var startPosition=312;
                for(var i=0; i<Number(json.hundreds);i++)
                {
                    startPosition-=30;
                    //$("#ones svg").append('  <circle cx="67" cy="'+ startPosition+'" r="13" stroke="#ff33cc" stroke-//width="1" fill="#cc3399" />');
                    var circle = d3.select("#hundreds svg").append("circle")
                                    .attr("r", "13")
                                    .attr("style", "fill:#9fdfbe;stroke:#26734b;stroke-width:1")
                                    .attr("cx","67")
                                    .attr("cy",startPosition);
                
                }
            }
    
        
           if(Number(json.Thousands)>0)
            {
                   var rect = d3.select("#thousands svg").append("rect")
                                    .attr("width", "5")
                                     .attr("height", "300")
                                    .attr("style", "fill:#9999ff;stroke-width:2;stroke:rgb(0,0,0)" )
                                    .attr("x","65")
                                    .attr("y","0");
              
                 var startPosition=312;
                for(var i=0; i<Number(json.Thousands);i++)
                {
                    startPosition-=30;
                    //$("#ones svg").append('  <circle cx="67" cy="'+ startPosition+'" r="13" stroke="#ff33cc" stroke-//width="1" fill="#cc3399" />');
                    var circle = d3.select("#thousands svg").append("circle")
                                    .attr("r", "13")
                                    .attr("style", "fill:#ffdab3;stroke:#e67700;stroke-width:1")
                                    .attr("cx","67")
                                    .attr("cy",startPosition);
                
                }
            }
        
           if(Number(json.TensOfThousands)>0)
            {
                   var rect = d3.select("#tenOfThousand svg").append("rect")
                                    .attr("width", "5")
                                     .attr("height", "300")
                                    .attr("style", "fill:#9999ff;stroke-width:2;stroke:rgb(0,0,0)" )
                                    .attr("x","65")
                                    .attr("y","0");
              
                 var startPosition=312;
                for(var i=0; i<Number(json.TensOfThousands);i++)
                {
                    startPosition-=30;
                    //$("#ones svg").append('  <circle cx="67" cy="'+ startPosition+'" r="13" stroke="#ff33cc" stroke-//width="1" fill="#cc3399" />');
                    var circle = d3.select("#tenOfThousand svg").append("circle")
                                    .attr("r", "13")
                                    .attr("style", "fill:#e6004c;stroke:#660022;stroke-width:1")
                                    .attr("cx","67")
                                    .attr("cy",startPosition);
                
                }
            }
        
        
        
        
        })
    
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

