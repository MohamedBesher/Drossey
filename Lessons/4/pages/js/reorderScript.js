$(function(){
    
    
     $('.close_msg').click(function() {
        $(".notification_msg").remove();
    });
     
 //read from dynamic script 
    var jsonFile = sessionStorage.getItem('jsonFile');
    var lesson_id=  sessionStorage.getItem('lessonId');
   
         loadScript("../Data/Lessons/"+lesson_id+"/"+jsonFile+".js",function (a, b) {}).onload = function ()
        {
          
    
           // var MyObject = JSON.parse(data);
            
          $(data).each(function(i,json){  
                    if(json.title != undefined  && json.inside_title != undefined)
                     $('#title').append('<h4>' + json.title + '</h4><hr/><h5>'+json.inside_title+'</h5>');
                    $('#lesson_title').append(json.pageHeader);
 
                     $("#noti_msg").append(json.prompt);
          });
                        
            var list= $(data.elements);
     
    
                        // Random Cards
                            var exists = [],randomNumber;
                            for(var l=0;l < list.length;l++) {
                            do {
                            randomNumber = Math.floor(Math.random()*list.length);  
                            } while (exists[randomNumber]);
                            exists[randomNumber] = true;
                                
                                list.eq(randomNumber).each(function(i,item){ 
                                   
                                    $("#cards").append("<div class='card' id='"+item+"'>"+item+"</div>");
                                    
                                   
                                 });
                            
                            }
    
    
                            // Random Slots
                            var exists = [],randomNumber;
                            for(var l=0;l < list.length;l++) {
                            do {
                            randomNumber = Math.floor(Math.random()*list.length);  
                            } while (exists[randomNumber]);
                            exists[randomNumber] = true;
                                
                                list.eq(randomNumber).each(function(i,item){ 
                                   
                                    $("#slots").append("<div class='slot' id='"+item+"'></div>");
                                   
                                 });
                            
                                 $("#numbers").append("<div class='number'>"+(l+1)+"</div>");

                            }

    
    
    
    
    
    $( ".card" ).draggable({ revert: "invalid" , drag: function(event, ui) { } });
        
        
         $(".slot").droppable({
      classes: {
        "ui-droppable-active": "ui-state-active",
        "ui-droppable-hover": "ui-state-hover"
      },
      drop:handleCardDrop,
            
      accept: function(d) {
          if($(d).attr("id")==$(this).attr("id"))
          {
                 return true;
    }},
    });
             
         };
    
});

function handleCardDrop( event, ui ) {
  var slot = $(this).attr("id");
  var card = ui.draggable.attr("id");


  if ( slot == card ) {
    ui.draggable.draggable( 'disable' );
    $(this).droppable( 'disable' );
    ui.draggable.position( { of: $(this), my: 'left top', at: 'left top' } );
    ui.draggable.draggable( 'option', 'revert', false );
    $(ui.draggable).css("background-color","#008c27;");
      $(ui.draggable).css("z-index","10");
    $(this).css("background-color","#008c27;");
  } 
  
  // If all the cards have been placed correctly then display a message
  // and reset the cards for another go ..

 

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