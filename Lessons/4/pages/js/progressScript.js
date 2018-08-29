 //var MyObject = JSON.parse(data);
$(function(){
    
    i=0;
    var red_var,blue_var=0,white_var,line_var_l;
    var x,oddrow;
    row=$("#row");
   // var MyObject = JSON.parse(data);
    
         $('.close_msg').click(function() {
        $(".notification_msg").remove();
    });
    //read from dynamic script 
    var jsonFile = sessionStorage.getItem('jsonFile');
    var lesson_id=  sessionStorage.getItem('lessonId');
   
         loadScript("../Data/Lessons/"+lesson_id+"/"+jsonFile+".js",function (a, b) {}).onload = function ()
        {
          $(data).each(function(i,json){  
                   if(json.title != undefined  && json.inside_title != undefined && json.title != undefined ){
                        $('#title').append('<h4>' + json.title + '</h4><hr/><h5>'+json.inside_title+'</h5>');  
                        $('#lesson_title').append(json.pageHeader);
                    }   
              $("#noti_msg").append(json.prompt);
          });
                        
         
    
           
            
          
    //draw first div
   $("<div class='step step_blue active' style='margin-right: 7%;'>"+data[0].steps[i]+"</div><canvas class='canvas' id='canvas_"+i+"'></canvas>").appendTo(row);
              };
    
    $('#progress').on('click',".active",function(){
        $(this).removeClass("active");
        i++;
        //draw new row
      
        if(i %3 == 0)
            {
            x = i;
            if(i == 3 || i == (oddrow +6)){
                $("#progress").append("<div id="+x+" dir='ltr'><canvas class='canvas customCanvas'></canvas></div>");
                row=$("#"+x);
                oddrow=i;
            }
            else
                $("#progress").append("<div id="+x+" ><canvas class='canvas customCanvas'></div>");
                 row=$("#"+x);
        }
        
        
        
           if(i >= data[0].steps.length)
             {
                 return;
             }
         else if(i == 0 || i == (blue_var+3))
           {
               
               $("<div class='step step_blue active'>"+data[0].steps[i]+"</div><canvas class='canvas' id='canvas_"+i+"'></canvas>").appendTo(row);
               blue_var=i;
               
               
           }
        
        else if(i == 2 || i == (red_var + 3) )
           {
               $("<div class='step step_red active'>"+data[0].steps[i]+"</div><canvas class='canvas' id='canvas_"+i+"'></canvas>").appendTo(row);
               red_var=i;
           }
        else if(i == 1)
            {
                Draw("canvas_0",(1));
                $("<div class='step step_white active'>"+data[0].steps[i]+"</div><canvas class='canvas' id='canvas_"+i+"'></canvas>").appendTo(row);        
               white_var=i;
            }
      else if(i == (white_var + 3) )
           { 
               
             $("<div class='step step_white active'>"+data[0].steps[i]+"</div><canvas class='canvas' id='canvas_"+i+"'></canvas>").appendTo(row);        
               white_var=i;
           }
      
         
        //draw line 
        if( i != (data[0].steps.length-1))
       {
           if(i == 2 || i == (line_var_l + 3))
        {
            
            DrawLine_arrow("canvas_"+i,(i+1),$(".step:last").parent().prop("dir"));
          
            line_var_l=i;
        }
        
        else
            {
              
                    Draw("canvas_"+i,(i+1));
           }
       }
        
      
        
    });
    
 

    
    
    
});
function Draw(canvasId,index){                
                var c = document.getElementById(canvasId);
                var ctx = c.getContext("2d");
                ctx.arc(150,80,45,0,2*Math.PI);            
                ctx.fillStyle ="#cce9ff";             
                ctx.fill()
                ctx.lineWidth = 10;
                ctx.stroke();
               
                // set the text
                ctx.font = ' 80px Arial';
                ctx.textAlign = 'center';
                ctx. textBaseline = 'middle';
                ctx.fillStyle = 'black';  // a color name or by using rgb/rgba/hex values
                ctx.fillText(index, 60, 30); // text and position
                
               ctx.globalCompositeOperation='destination-over';
               var m = document.getElementById(canvasId);
               var mtx = c.getContext("2d");
               mtx.moveTo(0,90);
               mtx.lineTo(400,90);
               mtx.lineWidth = 10;
               mtx.stroke();               
            }

function DrawLine_arrow(canvasId,index,dir) {
    
     var c = document.getElementById(canvasId);
     var ctx = c.getContext("2d");             
  /*   ctx.arc(50,90,45,0,2*Math.PI);
     ctx.fillStyle ="#cce9ff";
     ctx.fill()
    */ 
    if(dir != "ltr")
  {
      
     ctx.moveTo(88,90);
     ctx.lineTo(300,90);
    
     ctx.moveTo(80,85);
     ctx.lineTo(80,200);
    
    //arrow left ..
      ctx.moveTo(140,200);
     ctx.lineTo(40,120);
    
     //arrow right .. 
      ctx.moveTo(120,115);
     ctx.lineTo(80,150);
  }
    else
        {
        ctx.moveTo(5,90);
     ctx.lineTo(170,90);
    
     ctx.moveTo(170,85);
     ctx.lineTo(170,200);
    
    //arrow left ..
      ctx.moveTo(200,200);
     ctx.lineTo(130,110);
    
     //arrow right .. 
      ctx.moveTo(200,115);
     ctx.lineTo(170,150);
        }
    // set the text
                ctx.font = ' 80px Arial';
                ctx.textAlign = 'center';
                ctx. textBaseline = 'middle';
                ctx.fillStyle = 'black';  // a color name or by using rgb/rgba/hex values
                ctx.fillText(index, 60, 30); // text and position
    
     ctx.lineWidth = 10;
     ctx.stroke();
    
    
    
    
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


