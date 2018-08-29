var textData=[];
   var x;
$(function(){
         $('.close_msg').click(function() {
        $(".notification_msg").remove();
     });
     //read from dynamic script 
     var jsonFile = sessionStorage.getItem('jsonFile');
     var lesson_id=  sessionStorage.getItem('lessonId');
    loadScript("../Data/Lessons/"+lesson_id+"/"+jsonFile+".js",function (a, b) {}).onload = function(){
        
               if(data.title != undefined  && data.inside_title != undefined && data.pageHeader != undefined ){
                        $('#title').append('<h4>' + data.title + '</h4><hr/><h5>'+data.inside_title+'</h5>');  
                        $('#lesson_title').append(data.pageHeader);
                    }      
                       
                     $("#noti_msg").append(data.prompt);
                //Circle Data Set
                textData = [
                //start point and first equation and it's answer 
                { "x": 95, "y": 412, "text" : "Go","class":"firstStep","color":"white"  },
                { "x": 155, "y": 400, "text" : ""+data.equation[0]+"" ,"display": "none","class":"firstStep" ,"color":"blue"},
                { "x": 245, "y": 412, "text" : ""+data.answers[0]+"" ,"class":"SecondStep","color":"white"},
                 // end  start point and first equation
                    
                    
                    
                { "x": 245, "y":67, "text" : ""+getRandomInt(100,500)+"","color":"white" },
                { "x": 395, "y": 67, "text" : ""+getRandomInt(100,500)+"" ,"color":"white"},
                //2{ "x": 545, "y": 67, "text" : "620" ,"color":"white"},
                
                { "x": 170, "y": 237, "text" : ""+getRandomInt(100,500)+"" ,"color":"white"},
          
               
                //2{ "x": 620, "y": 237, "text" : "887","color":"white" },
                //3{ "x": 470, "y": 581, "text" : ""+data.answers[3]+"" ,"class":"fifthStep" ,"color":"white"}
                { "x": 773, "y": 237, "text" : ""+getRandomInt(100,500)+"" ,"color":"white"},

                
                //{ "x": 395, "y": 412, "text" : "580" ,"color":"white"},
               //2 { "x": 545, "y": 412, "text" : "87" ,"color":"white"},
                //4{ "x": 695, "y": 412, "text" : "887" ,"color":"white"},
               //4 {"x": 845, "y": 412, "text" : "887" ,"color":"white"},
                
                { "x": 170, "y": 581, "text" : ""+getRandomInt(100,500)+"" ,"color":"white"},
                //2{ "x": 320, "y": 581, "text" : "99" ,"color":"white"},
               // 2{ "x": 470, "y": 581, "text" : "887" ,"color":"white"},
                //{ "x": 620, "y": 581, "text" : "87" ,"color":"white"},
                //{ "x": 770, "y": 581, "text" : "887" ,"color":"white"},
                
                { "x": 245, "y": 757, "text" : ""+getRandomInt(100,500)+"" ,"color":"white"},
                //{ "x": 395, "y": 757, "text" : "887","color":"white" },
                { "x": 545, "y": 757, "text" : ""+getRandomInt(100,500)+"","color":"white" },
                { "x": 695, "y": 757, "text" : ""+getRandomInt(100,500)+"","color":"white" },
                { "x": 845, "y": 757, "text" : ""+getRandomInt(100,500)+"","color":"white" }
                ];
       /*  x=getRandomInt(1,5);*/
        var x=4;
        if(x == 1)
        {
    
                //equation
                textData.push({ "x": 250, "y": 365, "text" : ""+data.equation[1]+"","transform":"rotate(-65 230,320)","display": "none" ,"class":"SecondStep","color":"blue"});
                       
                textData.push( { "x": 390, "y": 227, "text" : ""+data.equation[2]+"" ,"display": "none" ,"class":"thirdStep","color":"blue"});
      
                textData.push({ "x": 535, "y": 227, "text" : ""+data.equation[3]+"" ,"display": "none" ,"class":"fourthStep","color":"blue"});
                
             
                //answer circle which sjow next equation
                textData.push({ "x": 320, "y": 237, "text" : ""+data.answers[1]+"" ,"class":"thirdStep" ,"color":"white"});
                
                textData.push({ "x": 470, "y": 237, "text" : ""+data.answers[2]+"" ,"class":"fourthStep","color":"white"});
                
                textData.push({ "x": 545, "y": 67, "text" : ""+data.answers[3]+"" ,"class":"fifthStep win","color":"white"});
            
                //empty circle
                textData.push({ "x": 620, "y": 237, "text" : ""+getRandomInt(100,500)+"","color":"white"});
            
                textData.push({ "x": 395, "y": 412, "text" : ""+getRandomInt(100,500)+"","color":"white"});
                textData.push({ "x": 545, "y": 412, "text" : ""+getRandomInt(100,500)+"","color":"white"});
                textData.push({ "x": 695, "y": 412, "text" : ""+getRandomInt(100,500)+"","color":"white"});
                textData.push({ "x": 845, "y": 412, "text" : ""+getRandomInt(100,500)+"","color":"white"});
            
                textData.push({ "x": 320, "y": 581, "text" : ""+getRandomInt(100,500)+"","color":"white"});
                textData.push({ "x": 470, "y": 581, "text" : ""+getRandomInt(100,500)+"","color":"white"});
                textData.push({ "x": 620, "y": 581, "text" : ""+getRandomInt(100,500)+"","color":"white"});
                textData.push({ "x": 770, "y": 581, "text" : ""+getRandomInt(100,500)+"","color":"white"});
            
                textData.push({ "x": 395, "y": 757, "text" : ""+getRandomInt(100,500)+"","color":"white"});
            
            }
        else if(x == 2)
        {
                //equation
                textData.push({ "x": 315, "y": 455, "text" : ""+data.equation[1]+"","display": "none" ,"class":"SecondStep","color":"blue"});
                
                textData.push({ "x": 400, "y": 615, "text" : ""+data.equation[2]+"","display": "none" ,"class":"thirdStep","color":"blue"});
                
                textData.push({ "x":473, "y": 455, "text" : ""+data.equation[3]+"","display": "none" ,"class":"fourthStep","color":"blue"});
                
                textData.push({ "x":625, "y": 368, "text" : ""+data.equation[4]+"","display": "none" ,"class":"fifthStep","color":"blue"});
                
                //answer circle which sjow next equation
                textData.push({ "x": 320, "y": 581, "text" : ""+data.answers[1]+"" ,"class":"thirdStep" ,"color":"white"});
                
                textData.push({ "x": 470, "y": 581, "text" : ""+data.answers[2]+"" ,"class":"fourthStep" ,"color":"white"});
                
                textData.push({ "x": 545, "y": 412, "text" : ""+data.answers[3]+"" ,"class":"fifthStep" ,"color":"white"});
                
                 textData.push({  "x": 620, "y": 237, "text" : ""+data.answers[4]+"" ,"class":"sixthStep win" ,"color":"white"});
            
                //empty circle
                textData.push({ "x": 545, "y": 67, "text" : ""+getRandomInt(100,500)+"","color":"white"});
            
                textData.push({ "x": 320, "y": 237, "text" : ""+getRandomInt(100,500)+"","color":"white"});
                textData.push({ "x": 470, "y": 237, "text" : ""+getRandomInt(100,500)+"","color":"white"});
                
                textData.push({ "x": 395, "y": 412, "text" : ""+getRandomInt(100,500)+"","color":"white"});
                textData.push({ "x": 695, "y": 412, "text" : ""+getRandomInt(100,500)+"","color":"white"});
                textData.push({ "x": 845, "y": 412, "text" : ""+getRandomInt(100,500)+"","color":"white"});
            
                textData.push({ "x": 620, "y": 581, "text" : ""+getRandomInt(100,500)+"","color":"white"});
                textData.push({ "x": 770, "y": 581, "text" : ""+getRandomInt(100,500)+"","color":"white"});
            
                textData.push({ "x": 395, "y": 757, "text" : ""+getRandomInt(100,500)+"","color":"white"});
            
            
            }
        else if(x == 3)
        {
             //equation
                textData.push({ "x": 235, "y": 553, "text" : ""+data.equation[1]+"","display": "none" ,"class":"SecondStep","color":"blue"});
                
                textData.push({ "x": 318, "y": 730, "text" : ""+data.equation[2]+"","display": "none" ,"class":"thirdStep","color":"blue"});
                
                textData.push({ "x": 397, "y": 635, "text" : ""+data.equation[3]+"","display": "none" ,"class":"fourthStep","color":"blue"});
                
                 textData.push({ "x": 545, "y": 553, "text" : ""+data.equation[4]+"","display": "none" ,"class":"fifthStep","color":"blue"});
            
                 textData.push({ "x": 700, "y": 635, "text" : ""+data.equation[5]+"","display": "none" ,"class":"sixthStep","color":"blue"});
                
                //answer circle which show next equation
                textData.push({ "x": 320, "y": 581, "text" : ""+data.answers[1]+"" ,"class":"thirdStep" ,"color":"white"});
                
                textData.push({"x": 395, "y": 757, "text" : ""+data.answers[2]+"" ,"class":"fourthStep" ,"color":"white"});
               
                textData.push({ "x": 470, "y": 581, "text" : ""+data.answers[3]+"" ,"class":"fifthStep" ,"color":"white"});
                
                textData.push({ "x": 620, "y": 581, "text" : ""+data.answers[4]+"" ,"class":"sixthStep" ,"color":"white"});
            
                textData.push({  "x": 770, "y": 581, "text" : ""+data.answers[5]+"" ,"class":"seventhStep win"  ,"color":"white"});
                //empty circles
                textData.push({ "x": 545, "y": 67, "text" : ""+getRandomInt(100,500)+"","color":"white"});
            
                textData.push({ "x": 320, "y": 237, "text" : ""+getRandomInt(100,500)+"","color":"white"});
                textData.push({ "x": 470, "y": 237, "text" : ""+getRandomInt(100,500)+"","color":"white"});
                textData.push({ "x": 620, "y": 237, "text" : ""+getRandomInt(100,500)+"","color":"white"});
            
                textData.push({ "x": 395, "y": 412, "text" : ""+getRandomInt(100,500)+"","color":"white"});
                textData.push({ "x": 545, "y": 412, "text" : ""+getRandomInt(100,500)+"","color":"white"});
                textData.push({ "x": 695, "y": 412, "text" : ""+getRandomInt(100,500)+"","color":"white"});
                textData.push({ "x": 845, "y": 412, "text" : ""+getRandomInt(100,500)+"","color":"white"});
            
        }
        else
        {
            //equation
            textData.push({ "x": 250, "y": 365, "text" : ""+data.equation[1]+"","transform":"rotate(-65 230,320)","display": "none" ,"class":"SecondStep","color":"blue"});
            
            textData.push( { "x": 390, "y": 260, "text" : ""+data.equation[2]+"" ,"display": "none" ,"class":"thirdStep","color":"blue"});
            
            textData.push( { "x": 457, "y": 450, "text" : ""+data.equation[3]+"" ,"display": "none" ,"class":"fourthStep","color":"blue"});
            
             textData.push({ "x": 580, "y": 365, "text" : ""+data.equation[4]+"","transform":"rotate(-65 560,320)","display": "none" ,"class":"fifthStep","color":"blue"});
            
            textData.push( {"x": 750, "y": 390, "text" : ""+data.equation[3]+"" ,"display": "none" ,"class":"sixthStep","color":"blue"});
            
            //answer circle which show next equation
            textData.push({ "x": 320, "y": 237, "text" : ""+data.answers[1]+"" 
            ,"class":"thirdStep" ,"color":"white"});
            
            textData.push({ "x": 395, "y": 412, "text" : ""+data.answers[2]+"" ,"class":"fourthStep" ,"color":"white"});
            
            textData.push({ "x": 545, "y": 412, "text" : ""+data.answers[3]+"" ,"class":"fifthStep" ,"color":"white"});
            
            textData.push({ "x": 695, "y": 412, "text" : ""+data.answers[4]+"" ,"class":"sixthStep" ,"color":"white"});
            
            textData.push({ "x": 845, "y": 412, "text" : ""+data.answers[5]+"" ,"class":"seventhStep win" ,"color":"white"});
            
            //empty circles
            textData.push({ "x": 545, "y": 67, "text" : ""+getRandomInt(100,500)+"","color":"white"});
            
            textData.push({ "x": 470, "y": 237, "text" : ""+getRandomInt(100,500)+"","color":"white"});
            textData.push({ "x": 620, "y": 237, "text" : ""+getRandomInt(100,500)+"","color":"white"});
            
            textData.push({ "x": 320, "y": 581, "text" : ""+getRandomInt(100,500)+"","color":"white"});
            textData.push({ "x": 470, "y": 581, "text" : ""+getRandomInt(100,500)+"","color":"white"});
            textData.push({ "x": 620, "y": 581, "text" : ""+getRandomInt(100,500)+"","color":"white"});
            textData.push({ "x": 770, "y": 581, "text" : ""+getRandomInt(100,500)+"","color":"white"});
            
            textData.push({ "x": 395, "y": 757, "text" : ""+getRandomInt(100,500)+"","color":"white"});
            
        }
        writeText(textData);
    };
            
      

 
        
     
     
     
     
            
          
     /*$("circle").on("click", function() {
         debugger;
  console.log(this);
 // d3.event.stopPropagation();
});*/
           
           
});
function getRandomInt(min, max) {
  min = Math.ceil(min);
  max = Math.floor(max);
  return Math.floor(Math.random() * (max - min)) + min;
}
function writeText(textData)
{
        //get the SVG Viewport
             var svgContainer =d3.select("svg");

            //Add the SVG Text Element to the svgContainer
            var text = svgContainer.selectAll("text")
                        .data(textData)
                        .enter()
                        .append("text");

            //Add SVG Text Element Attributes
            var textLabels = text
                .attr("x",function(d) { return d.x; })
                 .attr("y",function(d) { return d.y; })
                 .text(function(d) { return d.text; })
                .attr("transform",function(d) { return d.transform; })
                .attr("display",function(d) { return d.display; })
                .attr("font-family", "sans-serif")
                .attr("font-size", "18px")
                .attr("text-anchor" , "middle")
                .attr("fill", function(d) { return d.color; })
                .attr("class", function(d) { return d.class; })
                /*.attr("data-toggle", function(d) { return d.data-toggle; })
                .attr("data-target", function(d) { return d.data-target; })*/
                .on("click", function(d,i){
                     if(d.class=="firstStep"){
                         $("."+d.class).addClass("reactive");
                    }
                    else if(d.class=="SecondStep"){
                         if($(".reactive").length == 2)
                         $("."+d.class).addClass("reactive");
                    }
                    else if(d.class=="thirdStep"){
                         if($(".reactive").length == 4)
                         $("."+d.class).addClass("reactive");
                    }
                    else if(d.class=="fourthStep"){
                         if($(".reactive").length == 6)
                         $("."+d.class).addClass("reactive");
                    }
                    else if(d.class=="fifthStep"){
                         if($(".reactive").length == 8)
                             {
                                $("."+d.class).addClass("reactive");
                             }
                    }
                    else if(d.class=="sixthStep"){
                         if($(".reactive").length == 10)
                         $("."+d.class).addClass("reactive");
                    }
                    else if(d.class=="fifthStep win"){
                         
                        if($(".reactive").length == 8)
                             {
                                $(".win").attr("data-toggle", "modal");
                                $(".win").attr("data-target", "#myModal");
                             }
                         
                    }
                    else if(d.class=="sixthStep win"){
                         
                        if($(".reactive").length == 10)
                             {
                                $(".win").attr("data-toggle", "modal");
                                $(".win").attr("data-target", "#myModal");
                             }
                         
                    }
                    else if(d.class=="seventhStep win"){
                         
                        if($(".reactive").length == 12)
                             {
                                $(".win").attr("data-toggle", "modal");
                                $(".win").attr("data-target", "#myModal");
                             }
                         
                    }

                
                 $(".reactive").css("display","inline");
                });
    
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